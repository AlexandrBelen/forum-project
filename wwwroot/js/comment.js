function onchange(e) {
    var articleDiv = document.getElementById("comments");
    // створюєм елемент
    var elem = document.createElement("li");
    // створюємо текст для нього
    var commentText = document.getElementById("Comment-Body").value;
    var elemText = document.createTextNode(commentText);
    var userEmail = document.getElementById("NameOfAuthor").value;
    var userText = document.createTextNode(userEmail);
    var br = document.createElement("br");

    // додаєм текст в елемент в якості дочірнього елемента
    elem.appendChild(userText);
    elem.appendChild(br);
    elem.appendChild(elemText);
    elem.classList.add("comment");
    elem.classList.add("list-group-item");
    // додаєм елемент в ul
    articleDiv.appendChild(elem);
}

var formComment = document.getElementById("commentform");

formComment.addEventListener("submit", sendRequest);
formComment.addEventListener("submit", onchange);

// клік
function sendRequest(event) {

    event.preventDefault();
    var formData = new FormData(formComment);

    var request = new XMLHttpRequest();

    request.open("POST", formComment.action);

    request.onreadystatechange = function () {
        if (request.readyState == 4 && request.status == 200)
            document.getElementById("output").innerHTML = request.responseText;
    }
    request.send(formData);
}