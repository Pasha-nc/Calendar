﻿const getCalendarData = function () {

    let xhrM = new XMLHttpRequest(); // создаем объект, с помощью которого будем отправлять запрос

    const selMonth = document.getElementById('selectedMonthCell').innerHTML;

    xhrM.open("POST", "/calendar/getcalendardata/?selectedMonth=" + selMonth); // отправляем POST запрос

    xhrM.onload = () => {   // после загрузки ответа Response

        let response = JSON.parse(xhrM.response);

        var j = 1;

        const startDay = response.selectedMonthStartingDay;

        const totalDays = response.daysInMonth;

        for (var i = startDay; i < totalDays + startDay; i++) {

            document.querySelector("#calendarCell" + i.toString()).innerHTML = j;

            document.querySelector("#selectedMonthCell").innerHTML = response.selectedMonth.toString() + "." + response.selectedYear.toString();

            j++;
        }
    }
    xhrM.send();
}

getCalendarData();

const changeMonth = function (offset) {

    let xhrM = new XMLHttpRequest(); // создаем объект, с помощью которого будем отправлять запрос

    const selMonth = document.getElementById('selectedMonthCell').innerHTML;

    xhrM.open("POST", "/calendar/changemonth/?selectedMonth=" + selMonth + "&offset=" + offset); // отправляем POST запрос

    xhrM.onload = () => {   // после загрузки ответа Response

        let response = JSON.parse(xhrM.response);

        var j = 1;

        const startDay = response.selectedMonthStartingDay;

        const totalDays = response.daysInMonth;

        for (var i = 0; i < 42; i++) {

            if (i >= startDay && i < totalDays + startDay) {

                document.querySelector("#calendarCell" + i.toString()).innerHTML = j;

                j++;
            }
            else {
                document.querySelector("#calendarCell" + i.toString()).innerHTML = "";
            }


            document.querySelector("#selectedMonthCell").innerHTML = response.selectedMonth.toString() + "." + response.selectedYear.toString();


        }
    }
    xhrM.send();
}

document.getElementById('prevMonth').addEventListener("click", () => { changeMonth(-1) });

document.getElementById('nextMonth').addEventListener("click", () => { changeMonth(1) });