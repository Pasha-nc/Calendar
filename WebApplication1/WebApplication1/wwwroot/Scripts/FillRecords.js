const addRecordRow = function (x) {
    const recordsAddRow = document.getElementById('recordsAddRow');

    let recRow = document.createElement("tr");
    recRow.setAttribute("id", "recordRow" + x.toString());
    recRow.innerHTML = "<td class='recordsCell' id='idCell" + x.toString() + "'>iD</td><td class='recordsCell' colspan='8' id='recordCell" + x.toString() + "'>Record</td><td class='recordsCellStatus' colspan = '2' id='statusCell" + x.toString() + "'>Status</td><td class='recordsCell' id='delRecCell" + x.toString() + "'>del</td>";

    recordsAddRow.parentNode.insertBefore(recRow, recordsAddRow);
}

addRecordRow(0);
addRecordRow(1);
addRecordRow(2);

const delRecord = function (y) {
    console.log(document.getElementById("idCell" + y.toString()).innerHTML);    
}

delRecord(1);

const addStatusList = function () {
    const myCells = document.getElementsByClassName('recordsCellStatus');

    for (var i = 0; i < myCells.length; i++) {
        console.log(myCells.item(i));
        myCells.item(i).innerHTML = "<select class='selectStatus'><option>Status1</option><option>Status2</option><option>Status3</option></select>";
    }
}

addStatusList();