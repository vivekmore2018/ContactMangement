app.service('ContactApiServices', function ($http,RESOURCES) {
    this.getAllContacts = function () {
        return $http.get(RESOURCES.CONTACT_MODULE + '/GetContacts');// Calling the web api here  
    };
    this.CreateContact = function (contact) {
        return $http.post(RESOURCES.CONTACT_MODULE +  '/CreateContact', contact);
    };
    this.UpdateContact = function (contact) {
        return $http.post(RESOURCES.CONTACT_MODULE + '/UpdateContact', contact);
    };
});