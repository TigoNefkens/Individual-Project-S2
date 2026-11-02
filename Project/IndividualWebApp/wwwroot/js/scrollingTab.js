const scrollContainer = document.querySelector('.scroll-container');

scrollContainer.innerHTML += scrollContainer.innerHTML;

function autoScroll() {
    scrollContainer.scrollLeft += 1;
    if (scrollContainer.scrollLeft >= scrollContainer.scrollWidth / 2) {
        scrollContainer.scrollLeft = 0;
    }
}

setInterval(autoScroll, 20);