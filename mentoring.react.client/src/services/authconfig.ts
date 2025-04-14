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
        clientId: "d237dddb-5618-430f-b117-9d85c9bdd599", // z aplikacji klient
        authority: "https://login.microsoftonline.com/9188040d-6c67-4c5b-b112-36a304b66dad", // tenant ID z serwera
        knownAuthorities: ["login.microsoftonline.com"],
    },
    cache: {
        cacheLocation: BrowserCacheLocation.SessionStorage,
        storeAuthStateInCookie: false,
    },
};

const msalInstance = new PublicClientApplication(msalConfig);
msalInstance.initialize().then(() => {
    // Default to using the first account if no account is active on page load
    if (!msalInstance.getActiveAccount() && msalInstance.getAllAccounts().length > 0) {
        // Account selection logic is app dependent. Adjust as needed for different use cases.
        msalInstance.setActiveAccount(msalInstance.getAllAccounts()[0]);
    }

    // Optional - This will update account state if a user signs in from another tab or window
    msalInstance.enableAccountStorageEvents();

    msalInstance.addEventCallback((event: EventMessage) => {
        if (event.eventType === EventType.LOGIN_SUCCESS && (event.payload as AuthenticationResult)) {
            const authResult = event.payload as AuthenticationResult;
            const account = authResult.account;
            msalInstance.setActiveAccount(account);
        }
    });
});

export { msalInstance, msalConfig };
