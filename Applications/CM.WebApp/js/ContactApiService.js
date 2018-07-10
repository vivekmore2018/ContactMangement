app.service('ContactApiServices', function ($http) {
    this.getAllContacts = function () {
        return $http.get('http://localhost:59972/api/contact/GetContacts');// Calling the web api here  
    };
    this.CreateContact = function (contact) {
        return $http.post('http://localhost:59972/api/contact/CreateContact',contact);
    };
    this.UpdateContact = function (contact) {
        return $http.post('http://localhost:59972/api/contact/UpdateContact', contact);
    };
});