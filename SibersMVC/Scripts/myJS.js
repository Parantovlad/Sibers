$(function () {
    var dateFormat = "dd/mm/yy",
      start = $("#StartDate")
        .datepicker({
            defaultDate: "+1w",
            changeMonth: true,
            changeYear: true,
            numberOfMonths: 1
        })
        .on("change", function () {
            end.datepicker("option", "minDate", getDate(this));
        }),
      end = $("#EndDate").datepicker({
          defaultDate: "+1w",
          changeMonth: true,
          changeYear: true,
          numberOfMonths: 1
      })
      .on("change", function () {
          start.datepicker("option", "maxDate", getDate(this));
      });

    $.datepicker.regional["ru"] = {
        dateFormat: dateFormat,
        firstDay: 1,
        isRTL: false,
        showMonthAfterYear: false,
        yearSuffix: ""
    };
    $.datepicker.setDefaults($.datepicker.regional["ru"]);

    $.validator.methods.date = function (value, element) {
        return this.optional(element) || Globalize.parseDate(value, dateFormat, "ru");
    };

    function getDate(element) {
        var date;
        try {
            date = $.datepicker.parseDate(dateFormat, element.value);
        } catch (error) {
            date = null;
        }

        return date;
    };
});