function convertStringToDate(dateString) {
    //2017-24-03 11:10 AM This is the format we receive from the server
    var momentDate = moment(dateString, 'YYYY-MM-DD HH:mm tt');
    var jsDate = momentDate.toDate();
    
    return jsDate;
}