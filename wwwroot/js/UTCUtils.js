
var DATE_FORMAT_STRING = "mm/dd/yyyy h:MM:ss TT";
function formatToStandard(dateString) {
    return dateFormat(new Date(dateString), DATE_FORMAT_STRING);
}

function convertToUTC(LocalDateString) {

    // create a date object from local date
    var localDate = new Date(LocalDateString);
    // Simple and stupid, create UTC date object.
    var utcDate = new Date(
        localDate.getUTCFullYear(),
        localDate.getUTCMonth(),
        localDate.getUTCDate(),
        localDate.getUTCHours(),
        localDate.getUTCMinutes(),
        localDate.getUTCSeconds()
    );

    // format utc string to standard, then return.
    var dateTime = dateFormat(utcDate, DATE_FORMAT_STRING);

    return dateTime;
}

function convertToLocal(UTCDateString) {
    // the time in the parameter is in UTC,
    // first we convert it to a specially crafted format.
    var dateTime = formatToStandard(UTCDateString);
    
    // Then we append a UTC in front of the special format, and
    // then reconvert, this gives us the local time.
    var toReturn = formatToStandard(dateTime + " UTC");
    return toReturn;
}