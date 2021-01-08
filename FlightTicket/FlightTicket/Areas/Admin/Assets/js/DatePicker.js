
$(function () {

    $(document).ready(function () {
        $("#datepicker").datepicker({

            format: 'mm-yyyy',
            viewMode: "months",
            minViewMode: "months",
            autoclose: true
        }).datepicker('update', new Date());
        $('select').on('change', function () {
            debugger

            if (this.value == 1) {
                $("#datepicker").datepicker("remove");
                $("#datepicker").datepicker({
                    format: 'mm-yyyy',
                    viewMode: "months",
                    minViewMode: "months",
                    autoclose: true
                }).datepicker('update', new Date());

                $("#datepicker").datepicker("refresh");

            }
            else {

                $("#datepicker").datepicker("remove");

                $("#datepicker").datepicker({
                    format: 'yyyy',
                    viewMode: "years",
                    minViewMode: "years",
                    autoclose: true
                }).datepicker('update', new Date());
                $("#datepicker").datepicker("refresh");

            }

        });

        $('#datepicker').on('show', function (e) {

            if (e.date) {
                $(this).data('stickyDate', e.date);
            }
            else {
                $(this).data('stickyDate', null);
            }
        });

        $('#datepicker').on('hide', function (e) {
            debugger
            var stickyDate = $(this).data('stickyDate');

            if (!e.date && stickyDate) {
                $(this).datepicker('setDate', stickyDate);
                $(this).data('stickyDate', null);
            }
            else {
                var a = $('#selector').val();
                if (a == 1) {
                    var date = new Date($("#datepicker").datepicker('getDate')).toLocaleString("en-US");
                    $("#loading").show();
                    $("#mylineChart").hide();
                    $.ajax(
                        {
                            type: 'GET',
                            url: "/Reveneu/MonthChart",
                            data: { Date: date },
                            success: function (response) {
                                $("#mylineChart").show();
                                $("#reveneu").html(response);
                            },
                            complete: function () {
                                $('#loading').hide();
                            }
                        });
                }
                else {
                    var date = new Date($("#datepicker").datepicker('getDate')).toLocaleString("en-US");
                    $("#loading").show();
                    $("#mylineChart").hide();
                    $.ajax(
                        {
                            type: 'GET',
                            url: "/Reveneu/YearChart",
                            data: { Date: date },
                            success: function (response) {
                                console.log(response);
                                $("#mylineChart").show();
                                $("#reveneu").html(response);
                            },
                            complete: function () {
                                $('#loading').hide();
                            }
                        });
                }
            }

        });


    });

});