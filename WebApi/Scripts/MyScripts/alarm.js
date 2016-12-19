
function GetAllAlarm() {
    jQuery.support.cors = true;
    $.ajax({
        url: '/api/AlarmsAPI/',
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            WriteResponse(data);
        },
        error: function (x, y, z) {
            alert(x + '\n' + y + '\n' + z);
        }
    });
}

function AddAlarm() {
    jQuery.support.cors = true;
    var alarm = {
        Id: $('#Id').val(),
        Power: $('#PowerIsOn').val(),
        AlarmSignal: $('#AlarmSignal').val(),
        Time: $('#Time').val()
    };

    $.ajax({
        url: '/api/AlarmsAPI/',
        type: 'POST',
        data: JSON.stringify(alarm),
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            GetAllAlarm(data);
        },
        error: function (x, y, z) {
            alert(x + '\n' + y + '\n' + z);
        }
    });
}

function UpdateAlarm() {
    jQuery.support.cors = true;
    var alarm = {
        Id: $('#Id').val(),
        Power: $('#PowerIsOn').val(),
        AlarmSignal: $('#AlarmSignal').val(),
        Time: $('#Time').val()
    };

    $.ajax({
        url: '/api/AlarmsAPI/' + alarm.ID,
        type: 'PUT',
        data: JSON.stringify(alarm),
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            GetAllalarm(data);
        },
        error: function (x, y, z) {
            alert(x + '\n' + y + '\n' + z);
        }
    });
}

function DeleteAlarm() {
    jQuery.support.cors = true;
    var id = $('#txtId').val();

    $.ajax({
        url: 'http://localhost:60136//api/AlarmsAPI/' + id,
        type: 'DELETE',
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            GetAllAlarm(data);
        },
        error: function (x, y, z) {
            alert(x + '\n' + y + '\n' + z);
        }
    });
}

function WriteResponse(alarm) {
    var alarmResult = "<table><th>Id</th><th>Power</th><th>Alarm Signal</th><th>Time</th><tr>";
    $.each(alarm, function (index, alarm) {
        alarmResult += "<tr><td>" + alarm.Id + "</td><td> " + alarm.PowerIsOn + "</td><td>" + alarm.AlarmSignal + "</td><td>" + alarm.Time + "</td></tr>";
    });
    alarmResult += "</table>";
    $("#divResult").html(alarmResult);
}

function ShowAlarm(alarm) {
    if (alarm !== null) {
        var alarmResult = "<table><th>Id</th><th>Power</th><th>Alarm Signal</th><th>Time</th><tr>";
        alarmResult += "<tr><td>" + alarm.Id + "</td><td> " + alarm.PowerIsOn + "</td><td>" + alarm.AlarmSignal + "</td><td>" + alarm.Time + "</td></tr>";
        alarmResult += "</table>";
        $("#divResult").html(alarmResult);

        $('#Id').val(alarm.Id);
        $('#PowerIsOn').val(alarm.PowerIsOn);
        $('#AlarmSignal').val(alarm.AlarmSignal);
        $('#Time').val(alarm.Time);
    }
    else {
        $("#divResult").html("No Results To Display");
    }
}

function GetAlarm() {
    jQuery.support.cors = true;
    var id = $('#Id').val();
    $.ajax({
        url: '/api/alarmsAPI/' + id,
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            ShowAlarm(data);
        },
        error: function (x, y, z) {
            alert(x + '\n' + y + '\n' + z);
        }
    });
}
