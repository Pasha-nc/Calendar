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
    recCellRecord.setAttribute("class", "recordsCell");
    recCellStatus.setAttribute("class", "recordsCellStatus");
    recCellDel.setAttribute("class", "recordsCell");

    recCellId.setAttribute("id", "idCell" + recId.toString());
    recCellTime.setAttribute("id", "idCell" + recId.toString());
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


var myDate = "17:15";

addRecordRow(0, myDate, "Record");
addRecordRow(1, myDate, "Record");
addRecordRow(2, myDate, "Record");

const delRecord = function (y) {
    console.log(document.getElementById("idCell" + y.toString()).innerHTML);
}

delRecord(1);

const addStatusList = function () {
    const myCells = document.getElementsByClassName('recordsCellStatus');

    for (var i = 0; i < myCells.length; i++) {
        console.log(myCells.item(i));

        const mySelectStatus = document.createElement("select");
        mySelectStatus.setAttribute("class", "selectStatus");

        const selOption1 = document.createElement("option");
        const selOption2 = document.createElement("option");
        const selOption3 = document.createElement("option");

        selOption1.innerHTML = "Status1";
        selOption2.innerHTML = "Status2";
        selOption3.innerHTML = "Status3";

        myCells.item(i).append(mySelectStatus);

        mySelectStatus.append(selOption1);
        mySelectStatus.append(selOption2);
        mySelectStatus.append(selOption3);
    }
}

addStatusList();

const removeRecordRow = function () {
    const recRows = document.getElementsByClassName("recordRowClass");
    for (var i = recRows.length - 1; i >= 1; i--) {
        recRows.item(i).remove();
    }
}

//removeRecordRow();