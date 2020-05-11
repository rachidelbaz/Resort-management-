const btnTagolle = document.querySelector(".btn_Tagolle");
const links = document.querySelector(".links");

btnTagolle.addEventListener("click", () => {
  let value = links.classList.contains("links_collapse");
  if (value) {
    links.classList.remove("links_collapse");
    btnTagolle.classList.remove("change");
  } else {
    links.classList.add("links_collapse");
    btnTagolle.classList.add("change");
  }
});
