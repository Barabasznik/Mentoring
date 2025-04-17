import {
    BrowserCacheLocation,
    Configuration,
    EventMessage,
    EventType,
    AuthenticationResult,
    PublicClientApplication,
} from "@azure/msal-browser";

const msalConfig: Configuration = {
    auth: {
        clientId: "d237dddb-5618-430f-b117-9d85c9bdd599",
        authority: "https://login.microsoftonline.com/9188040d-6c67-4c5b-b112-36a304b66dad",
        knownAuthorities: ["login.microsoftonline.com"],
    },
    cache: {
        cacheLocation: BrowserCacheLocation.SessionStorage,
        storeAuthStateInCookie: false,
    },
};

const msalInstance = new PublicClientApplication(msalConfig);
msalInstance.initialize().then(() => {
    if (!msalInstance.getActiveAccount() && msalInstance.getAllAccounts().length > 0) {
        msalInstance.setActiveAccount(msalInstance.getAllAccounts()[0]);
    }

    msalInstance.enableAccountStorageEvents();

    msalInstance.addEventCallback((event: EventMessage) => {
        if (event.eventType === EventType.LOGIN_SUCCESS && (event.payload as AuthenticationResult)) {
            const authResult = event.payload as AuthenticationResult;
            msalInstance.setActiveAccount(authResult.account);
        }
    });
});

export { msalInstance, msalConfig };
