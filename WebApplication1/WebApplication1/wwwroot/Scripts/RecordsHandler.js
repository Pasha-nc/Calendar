const addNewRecord = function (myDate, title, status) {
    let xhrA = new XMLHttpRequest();

    const body = JSON.stringify({ MyDateTime: myDate, Title: title, Status: status });

    xhrA.open("POST", '/api/records', true);
    
    xhrA.setRequestHeader('Content-Type', 'application/json');

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

//2022-01-11T11:04:17+00:00
document.getElementById("inputRecSubmit").addEventListener("click", () => {
    const dateArr = document.getElementById("selDateHeader").innerHTML.split(".");

    const myDay = dateArr[2].toString() + "-" + (dateArr[1] < 10 ? "0" + dateArr[1] : dateArr[1]).toString() + "-" + (dateArr[0] < 10 ? "0" + dateArr[0] : dateArr[0]).toString();

    const myTime = document.getElementById("inputRecTime").value;
    const myTitle = document.getElementById("inputRecText").value;

    const myDate = myDay.toString() + "T" + myTime.toString() + ":00+00:00";

    addNewRecord(myDate, myTitle, 0);
});

const addDelClickEvent = function () {
    const delCells = document.getElementsByClassName("recordsCellDel");

    const selDateH = document.getElementById("selDateHeader").innerHTML;

    for (var i = 0; i < delCells.length; i++) {
        const myId = delCells.item(i).getAttribute("id").substring(7); //recordCell

        let delRec = new XMLHttpRequest();

        delCells.item(i).addEventListener("click", () => {
            
            delRec.open("DELETE", "/api/records/" + myId.toString());

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



