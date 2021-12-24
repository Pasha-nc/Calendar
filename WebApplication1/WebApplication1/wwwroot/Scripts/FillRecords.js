const addRecordRow = function () {
    const recordsAddRow = document.getElementById('recordsAddRow');

    let recRow = document.createElement("tr");
    recRow.innerHTML = "<td class='recordsCell'>iD</td><td class='recordsCell' colspan='5'>Record</td><td class='recordsCell'>delRec</td>";

    recordsAddRow.parentNode.insertBefore(recRow, recordsAddRow);
}

addRecordRow();
addRecordRow();
addRecordRow();
