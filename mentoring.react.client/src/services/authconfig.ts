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
        clientId: "c8e5e41f-17b3-44c6-b4c3-111dbab4a267",
        authority: "https://login.microsoftonline.com/e9f3cf59-6853-4d21-aeda-b602198c1ae2",
        redirectUri: "/",
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
