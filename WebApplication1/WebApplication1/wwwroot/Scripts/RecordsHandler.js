const addNewRecord = function (myDate, title, status) {
    let xhrA = new XMLHttpRequest();

    const body = 'myDate=' + encodeURIComponent(myDate) +
        '&title=' + encodeURIComponent(title) + '&status=' + encodeURIComponent(status);

    xhrA.open("POST", '/calendar/addrec', true);
    xhrA.setRequestHeader('Content-Type', 'application/x-www-form-urlencoded');

    xhrA.onload = () => {
        if (xhrA.status == 200) {            
            removeRecordRow();
            getUserRecords();
            document.getElementById("inputRecTime").value = "";
            document.getElementById("inputRecText").value = ""
        }
        else
        {
            alert("Something went wrong! Error " + xhrA.status);
        }
    };
        
    xhrA.send(body);    
}

document.getElementById("inputRecSubmit").addEventListener("click", () => {
    myDay = document.getElementById("selDateHeader").innerHTML;
    myTime = document.getElementById("inputRecTime").value;
    myTitle = document.getElementById("inputRecText").value;
    addNewRecord(myDay.toString() + " " + myTime.toString(), myTitle, "0");
});

const addDelClickEvent = function () {
    const delCells = document.getElementsByClassName("recordsCellDel");

    const selDateH = document.getElementById("selDateHeader").innerHTML;

    for (var i = 0; i < delCells.length; i++) {
        const myId = delCells.item(i).getAttribute("id").substring(7); //recordCell

        let delRec = new XMLHttpRequest();

        delCells.item(i).addEventListener("click", () => {
            

            delRec.open("DELETE", "/calendar/delRec?recId=" + myId.toString());
            delRec.onload = () => {
                if (delRec.status == 200) {
                    removeRecordRow();
                    getUserRecords();                    
                }
                else {
                    alert("Something went wrong! Error " + delRec.status);
                }
            };
            delRec.send();
        });
    }
    
}



