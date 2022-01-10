const addNewRecord = function (myDate, title, status) {
    let xhrA = new XMLHttpRequest();

    const body = 'myDate=' + encodeURIComponent(myDate) +
        '&title=' + encodeURIComponent(title) + '&status=' + encodeURIComponent(status);

    xhrA.open("POST", '/calendar/addrec', true);
    xhrA.setRequestHeader('Content-Type', 'application/x-www-form-urlencoded');

    xhrA.onload = () => {
        removeRecordRow();
        getUserRecords();
        document.getElementById("inputRecTime").value = "";
        document.getElementById("inputRecText").value = ""
    };
        
    xhrA.send(body);    
}

document.getElementById("inputRecSubmit").addEventListener("click", () => {
    myDay = document.getElementById("selDateHeader").innerHTML;
    myTime = document.getElementById("inputRecTime").value;
    myTitle = document.getElementById("inputRecText").value;
    addNewRecord(myDay.toString() + " " + myTime.toString(), myTitle, "0");
});

