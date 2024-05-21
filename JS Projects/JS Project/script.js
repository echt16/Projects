


let filmSearch = document.getElementById('filmSearch');
let searchButton = document.getElementById('searchButton');
let films = document.getElementById('films');
let noFound = document.getElementById('noFound');
let pagination = document.getElementById('pagination');
let forModal = document.getElementById('forModal');



let number = 1
searchButton.addEventListener('click', function () {
    films.innerHTML = '';
    noFound.innerHTML = '';
    pagination.innerHTML = '';
    if (filmSearch.value == '') {
        noFound.innerHTML = '<h1>Write the title</h1>'
    }

    fetch(`http://www.omdbapi.com/?i=tt3896198&apikey=c485b5ec&s=${filmSearch.value}&page=${number}`)
        .then(response => response.json())
        .then(response => addFilms(response))

        .then(function (response) {
            let quanityPages = Math.ceil(response.totalResults / 10);
            for (let i = 1; i <= quanityPages; i++) {

                pagination.innerHTML += `<button class="btnPag" onclick = "getPage(event)" id ="${i}">
                        ${i}
                        </button>`;
            }
            document.getElementsByClassName('btnPag')[0].style.background = 'white';
            document.getElementsByClassName('btnPag')[0].style.color = 'black';
            return response;
        })

        .catch(function (response) {
            if (response.Response == 'False' && filmSearch.value !== '') {
                noFound.innerHTML = '<h1>Nothing was found for your request... =&#40;</h1>'
            }
        })

})
function getPage(event) {
    let colRings = document.getElementsByClassName('btnPag');
    for (let i = 0; i < colRings.length; i++) {
        colRings[i].style.background = '';
        colRings[i].style.color = 'white';
    }

    const page = event.target.id;
    fetch(`http://www.omdbapi.com/?i=tt3896198&apikey=c485b5ec&s=${filmSearch.value}&page=${page}`)
        .then(response => response.json())
        .then(response => addFilms(response))


    colRings[event.target.id - 1].style.background = 'white';
    colRings[event.target.id - 1].style.color = 'black'
    number = event.target.id
}






function addFilms(response) {
    if (response.Response == 'False' && filmSearch.value !== '') {
        noFound.innerHTML = '<h1>Nothing was found for your request... =&#40;</h1>'
    }


    films.innerHTML = '';
    for (let i = 0; i < response.Search.length; i++) {
        if (response.Search[i].Poster !== 'N/A') {
            films.innerHTML += `
                        <div class="divFilm">
                            <div class="imgFilm"><img src=${response.Search[i].Poster}>
                                </div>
                                <div class="titleFilm">
                                    ${response.Search[i].Title}
                                    </div>
                                    <div class="divBtn">
                                        <button class="btnMoreFilm" onclick="moreInfo(event)" id="${response.Search[i].imdbID}">
                                            More
                                            </button>
                                            </div>
                                            </div>
                                            `
        }
        else {
            films.innerHTML += `
                        <div class="divFilm">
                            <div class="imgFilm"><img src="шрифты/notFound.jpg">
                                </div>
                                <div class="titleFilm">
                                    ${response.Search[i].Title}
                                    </div>
                                    <div class="divBtn">
                                        <button class="btnMoreFilm" onclick="moreInfo(event)" id="${response.Search[i].imdbID}">
                                            More
                                            </button>
                                            </div>
                                            </div>
                                            `
        }
    }

    return response;
}

function hiddenMoreInfo(){
    forModal.style.display = "none";
    document.querySelector("#logo").style.filter = "none";
    document.querySelector("#main").style.filter = "none";
    noFound.style.filter = "none";
    films.style.filter = "none";
    document.querySelectorAll(".center").item(0).style.filter = "none";
}

function moreInfo(event) {
    //были указаны только эти данные, поскольку докладную информацию (страна, автор и тд) апи не выдает 
    fetch(`http://www.omdbapi.com/?i=tt3896198&apikey=c485b5ec&s=${filmSearch.value}&page=${number}`)
        .then(response => response.json())
        .then(function (response) {
            forModal.style.display = "flex";
            document.querySelector("#logo").style.filter = "blur(100px)";
            document.querySelector("#main").style.filter = "blur(100px)";
            noFound.style.filter = "blur(100px)";
            films.style.filter = "blur(100px)";
            document.querySelectorAll(".center").item(0).style.filter = "blur(100px)";
            for (let i = 0; i < response.Search.length; i++) {
                console.log(event.target.id)
                if (event.target.id == response.Search[i].imdbID) {
                    if (response.Search[i].Poster !== 'N/A') {
                        forModal.innerHTML =
                            `
                    <div class="modalWindow">
                        <div class="modalDivImg">
                            <img src=${response.Search[i].Poster}>
                        </div>
                        <div class="modalDivInfo">

                            
                            <div></div><br>
                            <div class="divsInfo">Title:${response.Search[i].Title}</div>
                            <div class="divsInfo">Type: ${response.Search[i].Type}</div>
                            <div class="divsInfo">Year: ${response.Search[i].Year}</div>
                        </div>
                    </div>
                `
                    }
                    else {
                        forModal.innerHTML =
                            `
                    <div class="modalWindow">
                        <div class="modalDivImg">
                            <img src="шрифты/notFound.jpg">
                        </div>
                        <div class="modalDivInfo">
                            
                            <div></div><br>
                            <div class="divsInfo">Title:${response.Search[i].Title}</div>
                            <div class="divsInfo">Type: ${response.Search[i].Type}</div>
                            <div class="divsInfo">Year: ${response.Search[i].Year}</div>
                        </div>
                    </div>
                `
                        break;
                    }

                }
            }
        }
        );
}

