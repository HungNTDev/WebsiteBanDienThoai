window.initializeGoogleLogin = function (clientId, dotNetHelper) {
    google.accounts.id.initialize({
        client_id: clientId,
        callback: (response) => {
            dotNetHelper.invokeMethodAsync("OnGoogleLoginSuccess", response.credential);
        }
    });

    google.accounts.id.renderButton(
        document.getElementById("googleSignInDiv"),
        { theme: "outline", size: "large" }
    );
};
