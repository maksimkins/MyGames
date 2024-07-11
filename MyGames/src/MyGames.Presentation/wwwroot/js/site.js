// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

const arrowsRight = document.querySelectorAll(".arrow-right");
const arrowsLeft = document.querySelectorAll(".arrow-left");

const gameLists = document.querySelectorAll(".game-list");

arrowsRight.forEach((arrowRight, i) => {
  const itemNumber = gameLists[i].querySelectorAll("img").length;
  let clickCounter = 0;
  arrowRight.addEventListener("click", () => {
    const ratio = Math.floor(window.innerWidth / 270);
    clickCounter++;
    if (itemNumber - (4 + clickCounter) + (4 - ratio) >= 0) {
      gameLists[i].style.transform = `translateX(${
        gameLists[i].computedStyleMap().get("transform")[0].x.value - 350
      }px)`;
    } else {
      gameLists[i].style.transform = "translateX(0)";
      clickCounter = 0;
    }
  });

  console.log(Math.floor(window.innerWidth / 270));
});


//TOGGLE

const ball = document.querySelector(".toggle-ball");
const items = document.querySelectorAll(
  ".container,.game-list-title,.navbar-container,.sidebar,.left-menu-icon,.toggle,.comments-container,.comments"
);

ball.addEventListener("click", () => {
    items.forEach((item) => {
      item.classList.toggle("active");
    });
    ball.classList.toggle("active");
  });
  