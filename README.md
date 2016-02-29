# FMEPasswordManager
C# Password Manager using Json Repository

FMEPasswordManager.Api
=========================================================
Step 1:
------------------
var settings = { <br/>
  "async": true, <br/>
  "crossDomain": true, <br/>
  "url": "http://localhost/api/management/EncryptMasterKey", <br/>
  "method": "GET", <br/>
  "headers": { <br/>
    "content-type": "application/json", <br/>
    "x-masterkey": "Test1234" <br/>
  } <br/>
} <br/>

$.ajax(settings).done(function (response) { <br/>
  // encripted key for subsequent calls <br/>
  console.log(response); <br/>
}); <br/>

Step 2: 
--------------------
var settings = { <br/>
  "async": true, <br/>
  "crossDomain": true, <br/>
  "url": "http://localhost/api/passwordManager", <br/>
  "method": "GET", <br/>
  "headers": { <br/>
    "content-type": "application/json", <br/>
    "x-key": "JIqogbiWKXH7l8yazdRWCJBNy9p3N11w" // encripted key from response of step 1 <br/>
  } <br/>
} <br/>

$.ajax(settings).done(function (response) { <br/>
  console.log(response); <br/>
}); <br/>
<br/>

FMEPasswordManager.Web Frontend Dev Server and Automation
=========================================================

$ npm install
$ gulp
$ npm run watch

gulp command will bundle vendor files, transpile and bundle jsx files, and compile .less styles
npm run watch will start node server and watch all jsx, js, html, and style changes and update realtime output with nodemon
