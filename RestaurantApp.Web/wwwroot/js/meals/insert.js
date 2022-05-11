function SubmitMeal() {
    var data = $("#MealForm").serialize();
    $.ajax({
        type: 'POST',
        url: '/Meals/Create',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        data: data,
        success: function (result) {
            alert('Successfully received Data ');
            console.log(result);
        },
        error: function () {
            alert('Failed to receive the Data');
            console.log('Failed ');
        }
    })
}