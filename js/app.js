// import Something from "./another-js-file.js";
var bookSel;
class App
{
	constructor() {
		// Add onclick event to the key word search
        document.getElementById("btnSearch").addEventListener("click", function () { GetWordCount(); });
    }
    go() {
        // retrieve and display the list of books
        var url = '/api/books';
        $.ajax({
            url: url,
            type: 'GET',
            cache: true,
            error: function (x,h,r) {
                //Run code to handle error or use the global ajax extension method
            },
            success: function (data) {
                if (data) {
                    var str='';
                    $(data.BooksList).each(function (v, i) {
                        str += '<li data-val=' + i.BookId + '><div class="book-title"><label>' + i.BookTitle + '</label></div><div class="book-aut"><em>by ' + i.BookAuthor + '</em></div><div></li >';
                    });
                    $('#ulBookTiles').append(str);
                    loadTiles();//Add event listener for on click event
                }
            }            
        });
    }
    
}
new App().go();
//Add event listener to the books loaded
function loadTiles() {
    $('.ul-tiles li').each(function () {
        $(this).on('click', function () {
            bookSelect($(this));
        });
    });
}
//On book select event. Load the top 10 common words in the book
function bookSelect(t) {
    if (bookSel === $(t).data('val'))
        return;
    bookSel = $(t).data('val');
    $('#ulBookTiles li.tile-select').removeClass('tile-select');
    $('#ulBookTiles li[data-val="' + bookSel + '"]').addClass('tile-select');
    $('#h4CommonWords').html($(t).find('.book-title').html());
    //Clear word search grid/text
    ClearWordSearch();
    //load most used words in the book
    GetCommonWords(bookSel);
}
//Load the top 10 common words in the book if no key word to search is passed. Else, search the key words in the book
function GetCommonWords(id, search) {
    var url = '/api/books/' + id;
    var divId = '#divCommonWordsCount';
    if (search) { url = '/api/books/' + id + '/query=' + search; divId = '#divWordSearchCount'; }
    $.ajax({
        url: url,
        type: 'GET',
        cache: true,
        error: function (x, h, r) {
            
        },
        success: function (data) {
            if (data) {
                var str = '<em>Top 10 words</em><table class="table"><thead><tr><td>Word</td><td>Count</td></tr></thead></tbody>';
                $(data).each(function (v, i) {
                    str += '<tr><td>' + i.Word + '</td><td>' + i.Count + '</td></tr>';
                });
                str += '</tbody></table>';
                $(divId).html(str);
            }
        }
    });
}
//Validations
function GetWordCount() {
    if (!bookSel) {
        alert('Please select a book.');
        return;
    }
    var txt = $('#txtWordSearch').val();
    if (!txt || txt.length < 3) {
        alert('Please enter at least 3 characters to search');
        return;
    }
    GetCommonWords(bookSel, txt);
}
//Clear the keyword search/results when a new book is clicked
function ClearWordSearch() {
    $('#txtWordSearch').val('');
    $('#divWordSearchCount').html('');
}