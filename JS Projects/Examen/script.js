var books = [];

var visitors = [];

var cards = [];

document.querySelectorAll(".close").forEach(x => x.addEventListener("click", function () {
    x.parentElement.parentElement.style.display = "none";
}));

window.onload = function () {
    books = getBooksFromCookie();
    for (let i = 0; i < books.length; i++) {
        books[i].addInTable(document.querySelector("#bookTable"), false);
    }

    visitors = getVisitorsFromCookie();
    for (let i = 0; i < visitors.length; i++) {
        visitors[i].addInTable(document.querySelector("#visitorTable"), false);
    }

    cards = getCardsFromCookie();
    for (let i = 0; i < cards.length; i++) {
        cards[i].addInTable(document.querySelector("#cardTable"), false);
    }

    let top5Books = top5MostRepeatedBooks(books);
    for (let i = 0; i < top5Books.length; i++) {
        top5Books[i].addInTable(document.querySelector("#statistic1Table"), false);
    }

    let top5Visitors = top5MostRepeatedVisitors(visitors);
    for (let i = 0; i < top5Visitors.length; i++) {
        top5Visitors[i].addInTable(document.querySelector("#statistic2Table"), false);
    }
};



class Book {
    static classId = 0;
    constructor(name, author, year, publisher, pages, count) {
        this.id = ++Book.classId;
        this.name = name;
        this.author = author;
        this.year = year;
        this.publisher = publisher;
        this.pages = pages;
        this.count = count;
    }

    getObject() {
        return {
            ID: this.id,
            Name: this.name,
            Author: this.author,
            PublishDate: this.year,
            Publisher: this.publisher,
            Pages: this.pages,
            Count: this.count
        };
    }

    addInTable(table, bool) {
        var row = table.insertRow();
        let i = 0;
        let object = this.getObject();
        for (let item in object) {
            let cell = row.insertCell(i);
            cell.setAttribute("data-column", item);
            cell.innerHTML = object[item];
            i++;
        }
        let lastCell = row.insertCell(i);
        lastCell.style.cursor = "pointer";
        lastCell.onclick = editBook;
        lastCell.innerHTML = `
        <img src="edit.png" alt="edit">
        `;
        if (bool) {
            this.addInCookie();
        }
    }

    addInCookie() {
        let books = getBooksFromCookie();
        books.push(this);
        setCookie("books", JSON.stringify(books), 2);
    }
}

var bookIndex = -1;

function editBook(event) {
    let bookId = +event.target.parentElement.parentElement.children[0].innerHTML;
    bookIndex = +books.findIndex(x => x.id == bookId);
    document.querySelector("#bookNameEdit").value = books[bookIndex].name;
    document.querySelector("#bookAuthorEdit").value = books[bookIndex].author;
    document.querySelector("#bookYearEdit").value = books[bookIndex].year;
    document.querySelector("#bookPublisherEdit").value = books[bookIndex].publisher;
    document.querySelector("#bookPagesEdit").value = books[bookIndex].pages;
    document.querySelector("#bookCountEdit").value = books[bookIndex].count;
    document.querySelector("#bookEdit").style.display = "flex";
}

function saveEditBook(event) {
    event.preventDefault();


    let name = document.querySelector("#bookNameEdit").value;
    let author = document.querySelector("#bookAuthorEdit").value;
    let year = +document.querySelector("#bookYearEdit").value;
    let publisher = document.querySelector("#bookPublisherEdit").value;
    let pages = +document.querySelector("#bookPagesEdit").value;
    let count = +document.querySelector("#bookCountEdit").value;

    if (isNaN(year) || isNaN(pages) || isNaN(count) || year < 0 || year > 2025) {
        alert("Данные введены неправильно!");
        return;
    }

    setCookie("books", "");

    books[bookIndex].name = name;
    books[bookIndex].author = author;
    books[bookIndex].year = year;
    books[bookIndex].publisher = publisher;
    books[bookIndex].pages = pages;
    books[bookIndex].count = count;

    let table = document.querySelector("#bookTable");
    while (table.rows.length > 1) {
        table.deleteRow(1);
    }

    for (let i = 0; i < books.length; i++) {
        books[i].addInTable(table, true);
    }

    document.querySelector("#bookNameEdit").value = "";
    document.querySelector("#bookAuthorEdit").value = "";
    document.querySelector("#bookYearEdit").value = "";
    document.querySelector("#bookPublisherEdit").value = "";
    document.querySelector("#bookPagesEdit").value = "";
    document.querySelector("#bookCountEdit").value = "";
    document.querySelector("#bookEdit").style.display = "none";
    bookIndex = -1;
}

function addBook(event) {
    document.querySelector("#bookModal").style.display = "flex";
}

function saveBook(event) {
    event.preventDefault();

    let name = document.querySelector("#bookName").value;
    let author = document.querySelector("#bookAuthor").value;
    let year = +document.querySelector("#bookYear").value;
    let publisher = document.querySelector("#bookPublisher").value;
    let pages = +document.querySelector("#bookPages").value;
    let count = +document.querySelector("#bookCount").value;

    if (isNaN(year) || isNaN(pages) || isNaN(count) || year < 0 || year > 2025) {
        alert("Данные введены неправильно!");
        return;
    }

    books.push(
        new Book(name,
            author,
            year,
            publisher,
            pages,
            count)
    );

    books[books.length - 1].addInTable(document.querySelector("#bookTable"), true);
    document.querySelector("#bookName").value = "";
    document.querySelector("#bookAuthor").value = "";
    document.querySelector("#bookYear").value = "";
    document.querySelector("#bookPublisher").value = "";
    document.querySelector("#bookPages").value = "";
    document.querySelector("#bookCount").value = "";
    document.querySelector("#bookModal").style.display = "none";
}

function sortBook(event) {
    let table = document.querySelector("#bookTable");
    let columnName = removeSpaces(document.querySelector("#sortBook").value);
    if (columnName == "Name" || columnName == "Author" || columnName == "Publisher") {
        sortTableByText(table, columnName);
    }
    else {
        sortTableByNumber(table, columnName);
    }
}

function searchBook(event) {
    let table = document.querySelector("#bookTable");
    let text = document.querySelector("#searchingBook").value.toLowerCase();
    searchTextInTable(table, text);
}

function getBooksFromCookie() {

    let json = getCookie("books");

    if (json == "") {
        return [];
    }

    let obj = JSON.parse(json.trim());
    let array = [];

    obj.forEach(x => array.push(new Book(x.name, x.author, x.year, x.publisher, x.pages, x.count)));
    return array;
}
// -----------------------------------------------------------------------
class Visitor {
    static classId = 0;
    constructor(name, phone) {
        this.id = ++Visitor.classId;
        this.name = name;
        this.phone = phone;
    }

    getObject() {
        return {
            ID: this.id,
            Name: this.name,
            Phone: this.phone
        };
    }

    addInTable(table, bool) {
        var row = table.insertRow();
        let i = 0;
        let object = this.getObject();
        for (let item in object) {
            let cell = row.insertCell(i);
            cell.setAttribute("data-column", item);
            cell.innerHTML = object[item];
            i++;
        }
        let lastCell = row.insertCell(i);
        lastCell.style.cursor = "pointer";
        lastCell.onclick = editVisitor;
        lastCell.innerHTML = `
        <img src="edit.png" alt="edit">
        `;
        if (bool) {
            this.addInCookie();
        }
    }

    addInCookie() {
        let visitors = getVisitorsFromCookie();
        visitors.push(this);
        setCookie("visitors", JSON.stringify(visitors), 2);
    }
}

var visitorIndex = -1;

function editVisitor(event) {
    let visitorId = +event.target.parentElement.parentElement.children[0].innerHTML;
    visitorIndex = +visitors.findIndex(x => x.id == visitorId);
    document.querySelector("#visitorNameEdit").value = visitors[visitorIndex].name;
    document.querySelector("#visitorPhoneEdit").value = visitors[visitorIndex].phone;
    document.querySelector("#visitorEdit").style.display = "flex";
}

function saveEditVisitor(event) {
    event.preventDefault();


    let name = document.querySelector("#visitorNameEdit").value;
    let phone = document.querySelector("#visitorPhoneEdit").value;

    let phonePattern = /^(\+?\d{1,3})?[-. (]?\d{3}[-. )]?\d{3}[-. ]?\d{2}[-. ]?\d{2}$/;

    if (!phonePattern.test(phone)) {
        alert("Данные введены неправильно!");
        return;
    }

    setCookie("visitors", "");

    visitors[visitorIndex].name = name;
    visitors[visitorIndex].phone = phone;

    let table = document.querySelector("#visitorTable");
    while (table.rows.length > 1) {
        table.deleteRow(1);
    }

    for (let i = 0; i < visitors.length; i++) {
        visitors[i].addInTable(table, true);
    }

    document.querySelector("#visitorNameEdit").value = "";
    document.querySelector("#visitorPhoneEdit").value = "";
    document.querySelector("#visitorEdit").style.display = "none";
    visitorIndex = -1;
}

function addVisitor(event) {
    document.querySelector("#visitorModal").style.display = "flex";
}

function saveVisitor(event) {
    event.preventDefault();

    let name = document.querySelector("#visitorName").value;
    let phone = document.querySelector("#visitorPhone").value;

    let phonePattern = /^(\+?\d{1,3})?[-. (]?\d{3}[-. )]?\d{3}[-. ]?\d{2}[-. ]?\d{2}$/;

    if (!phonePattern.test(phone)) {
        alert("Данные введены неправильно!");
        return;
    }

    visitors.push(
        new Visitor(name, phone)
    );

    visitors[visitors.length - 1].addInTable(document.querySelector("#visitorTable"), true);
    document.querySelector("#visitorName").value = "";
    document.querySelector("#visitorPhone").value = "";
    document.querySelector("#visitorModal").style.display = "none";
}

function sortVisitor(event) {
    let table = document.querySelector("#visitorTable");
    let columnName = removeSpaces(document.querySelector("#sortVisitor").value);
    if (columnName == "Name" || columnName == "Phone") {
        sortTableByText(table, columnName);
    }
    else {
        sortTableByNumber(table, columnName);
    }
}

function searchVisitor(event) {
    let table = document.querySelector("#visitorTable");
    let text = document.querySelector("#searchingVisitor").value.toLowerCase();
    searchTextInTable(table, text);
}

function getVisitorsFromCookie() {
    let json = getCookie("visitors");

    if (json == "") {
        return [];
    }

    let obj = JSON.parse(json);
    let array = [];

    obj.forEach(x => array.push(new Visitor(x.name, x.phone)));
    return array;
}

// -------------------------------------------------

class Card {
    static classId = 0;
    constructor(visitor, book, borrowDate, returnDate = "return") {
        this.id = ++Card.classId;
        this.visitor = visitor;
        this.book = book;
        this.borrowDate = borrowDate;
        this.returnDate = returnDate;
    }

    getObject() {
        return {
            ID: this.id,
            Visitor: this.visitor.name,
            Book: this.book.name,
            BorrowDate: this.borrowDate,
            ReturnDate: this.returnDate
        };
    }

    addInTable(table, bool) {
        var row = table.insertRow();
        let i = 0;
        let object = this.getObject();
        for (let item in object) {
            if (item == "ReturnDate") {
                if (object[item] == "return") {
                    let lastCell = row.insertCell(i);
                    lastCell.style.cursor = "pointer";
                    lastCell.setAttribute("data-column", item);
                    lastCell.onclick = returnBook;
                    lastCell.innerHTML = `
                    <img src="return.png" alt="edit">
                    `;
                }
                else {
                    let cell = row.insertCell(i);
                    cell.setAttribute("data-column", item);
                    cell.innerHTML = object[item];
                }
            }
            else {
                let cell = row.insertCell(i);
                cell.setAttribute("data-column", item);
                cell.innerHTML = object[item];
            }
            i++;
        }
        if (bool) {
            this.addInCookie();
        }
    }

    addInCookie() {
        let cards = getCardsFromCookie();
        cards.push(this);
        setCookie("cards", JSON.stringify(cards), 2);
    }
}

function returnBook(event) {
    event.preventDefault();

    let cardId = +event.target.parentElement.parentElement.children[0].innerHTML;
    let cardIndex = cards.findIndex(x => x.id == cardId);

    setCookie("cards", "");
    
    cards[cardIndex].returnDate = formatDateForInputDate(new Date());
    
    let table = document.querySelector("#cardTable");
    while (table.rows.length > 1) {
        table.deleteRow(1);
    }
    
    for (let i = 0; i < cards.length; i++) {
        cards[i].addInTable(table, true);
    }
    
    setCookie("books", "");
    let bookTable = document.querySelector("#bookTable");
    let bookName = cards[cardIndex].book.name;

    let bookIndex = books.findIndex(x => x.name == bookName);

    books[bookIndex].count++;

    while (bookTable.rows.length > 1) {
        bookTable.deleteRow(1);
    }

    for (let i = 0; i < books.length; i++) {
        books[i].addInTable(bookTable, true);
    }
}

function getNamesFromArray(arr){
    let tmp = [];
    for(let i = 0; i < arr.length; i++){
        tmp.push(arr[i].name);
    }
    return tmp;
}

function addCard(event) {
    addOptionsToSelect("cardVisitor", getNamesFromArray(visitors));
    addOptionsToSelect("cardBook", getNamesFromArray(books));
    document.querySelector("#cardModal").style.display = "flex";
}

function saveCard(event) {
    event.preventDefault();

    let visitor = +visitors.findIndex(x => x.name == document.querySelector("#cardVisitor").value);
    let book = +books.findIndex(x => x.name == document.querySelector("#cardBook").value);
    let date = document.querySelector("#cardBorrowDate").value;

    cards.push(
        new Card(visitors[visitor], books[book], date)
    );

    cards[cards.length - 1].addInTable(document.querySelector("#cardTable"), true);
    document.querySelector("#cardModal").style.display = "none";

    setCookie("books", "");
    let bookTable = document.querySelector("#bookTable");
    let bookName = cards[cards.length - 1].book.name;

    let bookIndex = books.findIndex(x => x.name == bookName);

    books[bookIndex].count--;

    while (bookTable.rows.length > 1) {
        bookTable.deleteRow(1);
    }

    for (let i = 0; i < books.length; i++) {
        books[i].addInTable(bookTable, true);
    }
}

function formatDateForInputDate(date) {
    var year = date.getFullYear();
    var month = ('0' + (date.getMonth() + 1)).slice(-2);
    var day = ('0' + date.getDate()).slice(-2);
    return (year + '-' + month + '-' + day);
}

function getCardsFromCookie() {
    let json = getCookie("cards");

    if (json == "") {
        return [];
    }

    let obj = JSON.parse(json);
    let array = [];

    obj.forEach(x => array.push(new Card(x.visitor, x.book, x.borrowDate, x.returnDate)));
    return array;
}

function sortCard(event) {
    let table = document.querySelector("#cardTable");
    let columnName = removeSpaces(document.querySelector("#sortCard").value);
    if (columnName == "Visitor" || columnName == "Book") {
        sortTableByText(table, columnName);
    }
    else if (columnName == "ID") {
        sortTableByNumber(table, columnName);
    }
    else {
        sortTableByDate(table, columnName);
    }
}

function searchCard(event) {
    let table = document.querySelector("#cardTable");
    let text = document.querySelector("#searchingCard").value.toLowerCase();
    searchTextInTable(table, text);
}

function addOptionsToSelect(selectId, optionsArray) {
    var select = document.getElementById(selectId);

    if (!select) {
        console.error("Select element with ID '" + selectId + "' not found.");
        return;
    }

    optionsArray.forEach(function(optionText) {
        var option = document.createElement('option');
        option.text = optionText;
        option.value = optionText;
        select.add(option);
    });
}

// ------------------------------------------------------------------------

var choosings = document.querySelector("#choosing").getElementsByTagName("div");

for (let i = 0; i < choosings.length; i++) {
    choosings.item(i).addEventListener("click", function () {
        for (let i = 0; i < choosings.length; i++) {
            choosings.item(i).style.borderLeft = "1px solid #f6f6f635";
            choosings.item(i).style.borderRight = "0";
            if (i == choosings.length - 1) {
                choosings.item(i).style.borderRight = "1px solid #f6f6f635";
            }
        }

        if (i == 0) {
            document.querySelector("#statisticsContainer").style.display = "none";
            document.querySelector("#cardsContainer").style.display = "none";
            document.querySelector("#visitorsContainer").style.display = "none";
            document.querySelector("#booksContainer").style.display = "block";
        }
        if (i == 1) {
            document.querySelector("#statisticsContainer").style.display = "none";
            document.querySelector("#cardsContainer").style.display = "none";
            document.querySelector("#visitorsContainer").style.display = "block";
            document.querySelector("#booksContainer").style.display = "none";
        }
        if (i == 2) {
            document.querySelector("#statisticsContainer").style.display = "none";
            document.querySelector("#cardsContainer").style.display = "block";
            document.querySelector("#visitorsContainer").style.display = "none";
            document.querySelector("#booksContainer").style.display = "none";
        }
        if (i == 3) {
            document.querySelector("#statisticsContainer").style.display = "block";
            document.querySelector("#cardsContainer").style.display = "none";
            document.querySelector("#visitorsContainer").style.display = "none";
            document.querySelector("#booksContainer").style.display = "none";
        }

        this.style.borderLeft = "3px solid #03938f";
        this.style.borderRight = "3px solid #03938f";
    });
}

function sortTableByNumber(table, columnName) {
    let rows = Array.from(table.getElementsByTagName("tr"));
    rows.shift();
    rows.sort((a, b) => {
        let aValue = +a.querySelector(`td[data-column="${columnName}"]`).textContent.trim();
        let bValue = +b.querySelector(`td[data-column="${columnName}"]`).textContent.trim();
        return aValue - bValue;
    });
    while (table.rows.length > 1) {
        table.deleteRow(1);
    }
    rows.forEach(row => table.appendChild(row));
}


function sortTableByText(table, columnName) {
    let rows = Array.from(table.getElementsByTagName("tr"));
    rows.shift();
    rows.sort((a, b) => {
        let aValue = a.querySelector(`td[data-column="${columnName}"]`).textContent.trim();
        let bValue = b.querySelector(`td[data-column="${columnName}"]`).textContent.trim();
        return aValue.localeCompare(bValue);
    });
    while (table.rows.length > 1) {
        table.deleteRow(1);
    }
    rows.forEach(row => table.appendChild(row));
}


function sortTableByDate(table, columnName) {
    let rows = Array.from(table.getElementsByTagName("tr"));
    rows.shift();
    rows.sort((a, b) => {
        let aDateStr = a.querySelector(`td[data-column="${columnName}"]`).textContent.trim();
        let aValue = new Date(+aDateStr.split(".")[2], +aDateStr.split(".")[1] - 1, +aDateStr.split(".")[0]);

        let bDateStr = b.querySelector(`td[data-column="${columnName}"]`).textContent.trim();
        let bValue = new Date(+bDateStr.split(".")[2], +bDateStr.split(".")[1] - 1, +bDateStr.split(".")[0]);

        if (aValue > bValue) {
            return 1;
        }
        if (aValue < bValue) {
            return -1;
        }
        return 1;
    });
    while (table.rows.length > 1) {
        table.deleteRow(1);
    }
    rows.forEach(row => table.appendChild(row));
}

// --------------------------------------------------------

function removeSpaces(str) {
    return str.replace(/\s/g, '');
}

//---------------------------------------------------------

function searchTextInTable(table, searchText) {
    let tmp = Array.from(table.getElementsByTagName("tr"));
    let rows = [];
    tmp.shift();
    tmp.forEach(x => {
        let tmptd = Array.from(x.querySelectorAll("td"));
        tmptd.forEach(y => {
            if (y.innerHTML.toLowerCase().includes(searchText)) {
                rows.push(x);
            }
        });
    });
    while (table.rows.length > 1) {
        table.deleteRow(1);
    }
    rows.forEach(row => table.appendChild(row));
}

//-----------------------------------------------------------

function getCookie(key) {
    var cookies = document.cookie.split(';');

    for (var i = 0; i < cookies.length; i++) {
        var cookie = cookies[i].trim();
        if (cookie.indexOf(key + "=") === 0) {
            return cookie.substring(key.length + 1);
        }
    }

    return "";
}

function setCookie(key, value, expirationDays) {
    var expires = "";
    if (expirationDays) {
        var date = new Date();
        date.setTime(date.getTime() + (expirationDays * 24 * 60 * 60 * 1000));
        expires = "; expires=" + date.getTime();
    }

    var existingCookies = document.cookie.split(';');
    var updatedCookie = key + "=" + value + expires + "; path=/";
    var cookieUpdated = false;

    for (var i = 0; i < existingCookies.length; i++) {
        var cookie = existingCookies[i].trim();
        if (cookie.indexOf(key + "=") === 0) {
            document.cookie = updatedCookie;
            cookieUpdated = true;
            break;
        }
    }

    if (!cookieUpdated) {
        document.cookie = updatedCookie;
    }
}

// -------------------------------------------------
function top5MostRepeatedBooks(books) {
    const bookCounts = new Map();

    books.forEach(book => {
        const bookKey = `${book.name}-${book.author}`;
        if (bookCounts.has(bookKey)) {
            bookCounts.set(bookKey, bookCounts.get(bookKey) + 1);
        } else {
            bookCounts.set(bookKey, book.count);
        }
    });

    
    books.sort((a, b) => {
        const countA = bookCounts.get(`${a.name}-${a.author}`) || 0;
        const countB = bookCounts.get(`${b.name}-${b.author}`) || 0;
        return countB - countA;
    });

    
    return books.slice(0, 5);
}


function top5MostRepeatedVisitors(visitors) {
    const visitorCounts = new Map();

    visitors.forEach(visitor => {
        const visitorKey = `${visitor.name}-${visitor.phone}`;
        if (visitorCounts.has(visitorKey)) {
            visitorCounts.set(visitorKey, visitorCounts.get(visitorKey) + 1);
        } else {
            visitorCounts.set(visitorKey, 1);
        }
    });

    visitors.sort((a, b) => {
        const countA = visitorCounts.get(`${a.name}-${a.phone}`) || 0;
        const countB = visitorCounts.get(`${b.name}-${b.phone}`) || 0;
        return countB - countA;
    });

    return visitors.slice(0, 5);
}