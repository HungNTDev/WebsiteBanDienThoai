window.initializeGoogleLogin = function (dotNetHelper) {
    window.handleGoogleLogin = function (response) {
        const idToken = response.credential;
        dotNetHelper.invokeMethodAsync("HandleGoogleLoginCallback", idToken);
    };
};


