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
        clientId: "56b249e4-2c7a-4445-80d9-8a22401a7d3f",
        authority: "https://eventstorming.b2clogin.com/eventstorming.onmicrosoft.com/B2C_1_SignUpAndSignIn",
        knownAuthorities: ["eventstorming.b2clogin.com"],
    },
    cache: {
        cacheLocation: BrowserCacheLocation.SessionStorage, // This configures where your cache will be stored
        storeAuthStateInCookie: false, // Set this to "true" if you are having issues on IE11 or Edge
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
