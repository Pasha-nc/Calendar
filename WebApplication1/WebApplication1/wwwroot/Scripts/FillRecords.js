const addRecordRow = function (recId, recTime, recordText) {
    const recordsAddRow = document.getElementById('recordsAddRow');

    const recRow = document.createElement("tr");
    recRow.setAttribute("id", "recordRow" + recId.toString());

    recRow.setAttribute("class", "recordRowClass");

    const recCellId = document.createElement("td");
    const recCellTime = document.createElement("td");
    const recCellRecord = document.createElement("td");
    const recCellStatus = document.createElement("td");
    const recCellDel = document.createElement("td");

    recCellId.setAttribute("class", "recordsCell");
    recCellTime.setAttribute("class", "recordsCell");
    recCellRecord.setAttribute("class", "recordsCellTitle");
    recCellStatus.setAttribute("class", "recordsCellStatus");
    recCellDel.setAttribute("class", "recordsCellDel");

    recCellId.setAttribute("id", "idCell" + recId.toString());
    recCellTime.setAttribute("id", "timeCell" + recId.toString());
    recCellRecord.setAttribute("id", "recordCell" + recId.toString());
    recCellStatus.setAttribute("id", "statusCell" + recId.toString());
    recCellDel.setAttribute("id", "delCell" + + recId.toString());

    recCellRecord.setAttribute("colspan", "8");
    recCellStatus.setAttribute("colspan", "2");

    recCellId.setAttribute("hidden", "true");

    recCellId.innerHTML = recId;
    recCellTime.innerHTML = recTime;
    recCellRecord.innerHTML = recordText;
    recCellDel.innerHTML = "del";

    recordsAddRow.parentNode.insertBefore(recRow, recordsAddRow);

    recRow.append(recCellId);
    recRow.append(recCellTime);
    recRow.append(recCellRecord);
    recRow.append(recCellStatus);
    recRow.append(recCellDel);
}

const addStatusList = function () {
    const myCells = document.getElementsByClassName('recordsCellStatus');

    for (var i = 0; i < myCells.length; i++) {

        const mySelectStatus = document.createElement("select");
        mySelectStatus.setAttribute("class", "selectStatus");

        const selOption1 = document.createElement("option");
        const selOption2 = document.createElement("option");
        const selOption3 = document.createElement("option");

        selOption1.innerHTML = "ToStart";
        selOption2.innerHTML = "InProgress";
        selOption3.innerHTML = "Done";

        selOption1.setAttribute("id", "statusOptionToStart" + i.toString());
        selOption2.setAttribute("id", "statusOptionInProgress" + i.toString());
        selOption3.setAttribute("id", "statusOptionDone" + i.toString());

        myCells.item(i).append(mySelectStatus);

        mySelectStatus.append(selOption1);
        mySelectStatus.append(selOption2);
        mySelectStatus.append(selOption3);
    }
}

const setStatusList = function (recNum, recStatus) {
    const myCells = document.getElementsByClassName('recordsCellStatus');

    for (var i = 0; i < myCells.length; i++) {
        const selOpt = document.getElementById("statusOption" + recStatus + i.toString());

        if (i == recNum) {
            selOpt.setAttribute("selected", "true");

        }
    }
}

const addRecordsClickEvent = function () {
    const titleCells = document.getElementsByClassName("recordsCellTitle");    

    for (var i = 0; i < titleCells.length; i++) {
        const myId = titleCells.item(i).getAttribute("id").substring(10); //recordCell

        let getDescr = new XMLHttpRequest();

        titleCells.item(i).addEventListener("click", () => {

            let selDateH = document.getElementById("selDateHeader").innerHTML;

            getDescr.open("GET", "/api/records/" + myId.toString() + "/?mydate=" + selDateH);
            getDescr.onload = () => {
                let response = JSON.parse(getDescr.response);

                document.querySelector("#editIdCell").innerHTML = response.id;
                document.querySelector("#editDateCell").innerHTML = selDateH;

                document.querySelector('input[name = "editTimeInput"]').value = response.myDateTime.toString().substring(11, 16);
                document.querySelector('input[name = "editTitleInput"]').value = response.title;
                document.querySelector('textarea[name = "editDescInput"]').value = response.description;

                document.querySelector("#editToStart").removeAttribute("selected");
                document.querySelector("#editInProgress").removeAttribute("selected");
                document.querySelector("#editDone").removeAttribute("selected");

                document.querySelector("#edit" + response.status).setAttribute("selected", "true");
            }
            getDescr.send();

        });
    }
}

const getUserRecords = function () {
    let xhrR = new XMLHttpRequest();
        
    xhrR.open("GET", "/api/records/?selDate=" + document.getElementById("selDateHeader").innerHTML);

    xhrR.onload = () => {   // после загрузки ответа Response

        if (xhrR.response != "null") {

            let response = JSON.parse(xhrR.response);

            for (var i = 0; i < response.length; i++) {
                addRecordRow(response[i].id, response[i].myDateTime.toString().substring(11, 16), response[i].title);
            }

            addStatusList();

            for (var i = 0; i < response.length; i++) {

                setStatusList(i, response[i].status);
            }

            addRecordsClickEvent();

            addDelClickEvent();
        }        
    }
    xhrR.send();
}

getUserRecords();

const removeRecordRow = function () {
    const recRows = document.getElementsByClassName("recordRowClass");

    for (var i = recRows.length - 1; i >= 0; i--) {
        recRows.item(i).remove();
    }
}