$("#GoToHome").click(function() {
    document.location.href = '/Dashboard/Home';
});

function ShowWaiting() {
    StartWaiting('loaderImage');
    $("#WaitingContiner").show();
}

function HideWaiting() {
    $("#WaitingContiner").hide();
    StopWaiting();
}