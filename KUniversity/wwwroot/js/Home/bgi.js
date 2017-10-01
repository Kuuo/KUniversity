
var bgi = document.getElementById("rbgi");

var kBgiUrl = ",url(../../images/Home/KUniversityLoginBgi.png)";

var bgiList = [
    "url(../../images/BGI/1.jpg)",
    "url(../../images/BGI/2.jpg)",
    "url(../../images/BGI/3.jpg)",
    "url(../../images/BGI/4.jpg)"];

var index = parseInt(Math.random() * 4);

console.log(index);

bgi.style.backgroundImage = bgiList[index];
