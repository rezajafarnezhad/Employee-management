

window.confirm = function (_text, _title, _yesAction = function () { }) {
    swal.fire({
        title: _title,
        text: _text,
        icon: 'warning',
        showCancelButton: true,
        cancelButtonColor: '#3085d6',
        confirmButtonColor: '#d33',
        confirmButtonText: 'باشه',
        cancelButtonText: 'نه'
    }).then((result) => {
        if (result.isConfirmed) {
            _yesAction();
        }
    });
}

function SendData(_url, _data, _Funcs_Success = function (res) { }) {

    $.ajax({
        type: "post",
        enctype: 'multipart/form-data',
        url: _url,
        data: _data,
        timeout: 600000,
        beforeSend: function (xhr) {
           /* $('.loading').show();*/

            //var securityToken = $("[name=__RequestVerificationToken]").val();
            //xhr.setRequestHeader("XSRF-TOKEN", securityToken);
        },
        success: function (response) {
            _Funcs_Success(response);
        },
        complete: function (data) {
         /*   $('.loading').hide(3000);*/
        },
        error: function (data) {
            if (data.status == 429) {
                Alert429();
            }
        }
    });
}

function SendForm(_url, _FormId, _Funcs_Success = function (res) { }) {
    var form = $('#' + _FormId)[0];
    var _formdata = new FormData(form);

    $.ajax({
        type: "post",
        enctype: 'multipart/form-data',
        url: _url,
        data: _formdata,
        processData: false,
        contentType: false,
        cache: false,
        timeout: 600000,
        beforeSend: function (xhr) {

            $('.loading').show();

            var securityToken = $("[name=__RequestVerificationToken]").val();
            xhr.setRequestHeader("XSRF-TOKEN", securityToken);
        },
        success: function (response) {
            _Funcs_Success(response);
        },
        complete: function (data) {
            $('.loading').hide(100);
        },
        error: function (data) {
            if (data.status == 429) {
                Alert429();
            }
        }
    });
}

function LoadComponenet(_Url, _Data, _CallbackFuncs = function (data) { }) {
    $.ajax({
        url: _Url,
        type: 'get',
        data: _Data,
        beforeSend: function (xhr) {
            $('.loading').show();

        },
        complete: function (data) {
            $('.loading').hide(50);

        },
        error: function (data) {
            if (data.status == 429) {
                Alert429();
            }
        }
    }).done(function (data) {
        _CallbackFuncs(data);
    });
}

function RemoveData(_Url, _Data = {}) {
    confirm('آیا از حذف این مورد مطمئن هستید؟ ', 'پیام', function () {
        SendData(_Url, _Data);
    });
}

function StatusData(_Url, _Data = {}) {
    confirm('آیا از تغییر وضعیت این مورد مطمئن هستید؟ ', 'پیام', function () {
        SendData(_Url, _Data);
    });
}

function RefreshGrid(_GridId) {
    $('#' + _GridId).data("kendoGrid").dataSource.read();
    $('#' + _GridId).data("kendoGrid").refresh();
}

function ChangeUrl(_NewUrl) {
    history.pushState({}, null, _NewUrl);
}

function Alert429() {
    return swal.fire({
        title: '429',
        html: $.parseHTML(Err429Msg)[0].data,
        icon: 'warning',
        confirmButtonText: OkText
    });
}

function Logout(_ReturnUrl) {
    document.cookie = 'PrancaBeautyAuth=; expires=Thu, 01-Jan-70 00:00:01 GMT; path=/;';
    location.href = _ReturnUrl;
}

function ReloadPage() {
    location.reload();
}

//Disable pasting text into HTML form

(function () {
    var onload = window.onload;

    window.onload = function () {
        if (typeof onload == "function") {
            onload.apply(this, arguments);
        }

        var fields = [];
        var inputs = document.getElementsByTagName("input");
        var textareas = document.getElementsByTagName("textarea");

        for (var i = 0; i < inputs.length; i++) {
            fields.push(inputs[i]);
        }

        for (var i = 0; i < textareas.length; i++) {
            fields.push(textareas[i]);
        }

        for (var i = 0; i < fields.length; i++) {
            var field = fields[i];

            if (typeof field.onpaste != "function" && !!field.getAttribute("onpaste")) {
                field.onpaste = eval("(function () { " + field.getAttribute("onpaste") + " })");
            }

            if (typeof field.onpaste == "function") {
                var oninput = field.oninput;

                field.oninput = function () {
                    if (typeof oninput == "function") {
                        oninput.apply(this, arguments);
                    }

                    if (typeof this.previousValue == "undefined") {
                        this.previousValue = this.value;
                    }

                    var pasted = (Math.abs(this.previousValue.length - this.value.length) > 1 && this.value != "");

                    if (pasted && !this.onpaste.apply(this, arguments)) {
                        this.value = this.previousValue;
                    }

                    this.previousValue = this.value;
                };

                if (field.addEventListener) {
                    field.addEventListener("input", field.oninput, false);
                } else if (field.attachEvent) {
                    field.attachEvent("oninput", field.oninput);
                }
            }
        }
    }
})();