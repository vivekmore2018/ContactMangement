app.constant('RESOURCES', (function () {
    // Define your variable
    var resource = 'http://localhost:59972';
    // Use the variable in your constants
    return {
        USERS_DOMAIN: resource,
        USERS_API: resource + '/api',
        CONTACT_MODULE: resource + '/api/contact'
    }
})());