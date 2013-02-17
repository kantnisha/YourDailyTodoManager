$(function () {
    hideOverLay();
    getAllTasks();
    $('.addtask').click(function (evt) {
        evt.preventDefault();
        var url = "Task/AddTask";
        var data = { task: $('.taskinput').val() };
        showOverLay();
        $.post(url, data, function (rdata) {
            $('.alltodo').html(rdata);
            makeCompletedTasksGreen();
            $('.taskinput').val('').focus();
            $('.alltodo li.task:first').fadeOut(500).fadeIn(1000);
            hideOverLay();
        });
        return false;
    });

    $('a.action-anchor').live('click', function (evt) {
        evt.preventDefault();
        var url = "Task/DeleteTask";
        var param = { taskId: $(this).parent().siblings('.taskid').val() };
        $('.alltodo').attr('disabled', 'disabled');
        showOverLay();
        $.post(url, param, function (rdata) {
            $('.alltodo').html(rdata);
            makeCompletedTasksGreen();
            $('.alltodo').removeAttr('disabled');
            hideOverLay();
        });

        return false;
    });

    $('a.action-anchor-add').live('click', function (evt) {
        evt.preventDefault();
        var $this = $(this);
        if ($this.parent().siblings('.taskstate').children('.tstate').val() !== "1") {
            var url = "Task/CompleteTask";
            var param = { taskId: $this.parent().siblings('.taskid').val() };

            showOverLay();
            $.post(url, param, function (rdata) {
                if (rdata) {
                    $this.attr("title", "already marked complete");
                    $this.parent().siblings('.taskstate').children('.tstate').val("1");
                    $this.parent().parent().css("border-right-color", "green");
                    moveCompletedTaskBelow($this.parent().parent());
                    hideOverLay();
                }
            });
        }
        else {
            $this.attr("title", "already marked complete");
        }
        return false;
    });

    $('img.action-buttons')
        .live('mouseover', function () {
            $(this).addClass('bright');
        })
        .live('mouseout', function () {
            $(this).removeClass('bright');
        });

    function getAllTasks() {
        showOverLay();
        $.ajax({
            url: "/Task/GetAllTask",
            type: "GET",
            cache: false,
            success: function (data) {
                $('.alltodo').html(data);
                makeCompletedTasksGreen();
                hideOverLay();
            }
        });
    }

    function makeCompletedTasksGreen() {
        var $taskState = $('.tstate');
        $taskState.each(function (index) {
            if ($(this).val() === "1") {
                $(this).parent().parent().css("border-right-color", "green");
            }
        });
    }

    function moveCompletedTaskBelow($task) {
        $('ul.noindent').append($task);
        $task.fadeIn(1000);
    }

    function showOverLay() {
        $('.ajax-overlay').show();
    }
    function hideOverLay() {
        $('.ajax-overlay').hide();
    }
});