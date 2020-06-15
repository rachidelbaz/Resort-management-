//var jQueryScript = document.createElement('script');
//jQueryScript.setAttribute('src', 'https://code.jquery.com/ui/1.12.1/jquery-ui.min.js');
//document.head.appendChild(jQueryScript);
$(document).ready(function () {

    initDatePicker();

    function initDatePicker() {
        
        var checkIn;
        var checkOut;
        if ($('.datepicker').length) {
            let datePickers = $('.datepicker');
            datePickers.each(function () {
                let picker = $(this);
                if (picker.hasClass("booking_in")) {
                    checkIn = picker;
                } else {
                    checkOut = picker;
                }
            });
        }
         checkIn.datepicker({
            dateFormat: "yy-mm-dd",
            showAnim: "drop",
            minDate: new Date(),
            onSelect: function (selectedDate) {
                let d = new Date(selectedDate);
                d.setDate(d.getDate() + 1);
                checkOut.datepicker("option", "minDate", d.ShortStringDate(d));
            },
        });
        checkOut.datepicker({
            dateFormat: "yy-mm-dd",
            showAnim: "drop",
            minDate: new Date(),
            onSelect: function (selectedDate) {
                let d = new Date(selectedDate);
                d.setDate(d.getDate() - 1);
                checkIn.datepicker("option", "maxDate", d.ShortStringDate(d));
            },
        });
        Date.prototype.ShortStringDate = function (D) {
            let Year = D.getFullYear();
            let Month = D.getMonth() + 1;
            let Day = D.getDate();
            return Year.toString().concat("-", Month, "-", Day);
        };
    }

});