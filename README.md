# FMEPasswordManager
C# Password Manager using Json Repository

FMEPasswordManager.Api
=========================================================
Step 1:
------------------
var settings = {
  "async": true,
  "crossDomain": true,
  "url": "http://localhost/api/management/EncryptMasterKey",
  "method": "GET",
  "headers": {
    "content-type": "application/json",
    "x-masterkey": "Test1234"
  }
}

$.ajax(settings).done(function (response) {
  // encripted key for subsequent calls
  console.log(response);
});

Step 2:
--------------------
var settings = {
  "async": true,
  "crossDomain": true,
  "url": "http://localhost/api/passwordManager",
  "method": "GET",
  "headers": {
    "content-type": "application/json",
    "x-key": "JIqogbiWKXH7l8yazdRWCJBNy9p3N11w" // encripted key from response of step 1
  }
}

$.ajax(settings).done(function (response) {
  console.log(response);
});


FMEPasswordManager.Web Frontend Dev Server and Automation
=========================================================

$ npm install
$ gulp
$ npm run watch

gulp command will bundle vendor files, transpile and bundle jsx files, and compile .less styles
npm run watch will start node server and watch all jsx, js, html, and style changes and update realtime output with nodemon
