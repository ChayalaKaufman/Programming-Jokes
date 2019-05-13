$(() => {
    let firstTime = true;
    let like;

    $("#dislike-joke").on('click', function () {
        like = false;
        setLike();
    })

    $("#like-joke").on('click', () => {
        like = true;
        setLike();
    });

    function setLike() {
        const userId = $("#user-id").val();
        const jokeId = $("#like-joke").data('joke-id');

        $.post('/home/like', { jokeId, userId, liked: like }, () => {
            $("#like-joke").prop('disabled', like);
            $("#dislike-joke").prop('disabled', !like);

            if (firstTime) {
                setTimeout(disableButtons, 10000)
            }
            firstTime = false;
        });
    }

    function disableButtons() {
        $("#like-joke").prop('disabled', true);
        $("#dislike-joke").prop('disabled', true);
    }

})