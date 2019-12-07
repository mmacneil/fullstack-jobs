const host = location.protocol + '//' + location.hostname + (location.port ? ':' + location.port : '');
 
var config = {
    authority: host,
    client_id: 'js_test_client',
    redirect_uri: host + '/test-client/callback.html',
    response_type: "code",
    scope: "openid profile email api.read",
    filterProtocolClaims: true,
    loadUserInfo: true
};

var mgr = new Oidc.UserManager(config);

mgr.getUser().then(function (user) {
    if (user) {
        log("User logged in", user.profile);
    }
    else {
        log("User not logged in");
    }
});

function login() {
    mgr.signinRedirect();
}

function log() {
    document.getElementById('results').innerText = '';

    Array.prototype.forEach.call(arguments, function (msg) {
        if (msg instanceof Error) {
            msg = "Error: " + msg.message;
        }
        else if (typeof msg !== 'string') {
            msg = JSON.stringify(msg, null, 2);
        }
        document.getElementById('results').innerHTML += msg + '\r\n';
    });
}