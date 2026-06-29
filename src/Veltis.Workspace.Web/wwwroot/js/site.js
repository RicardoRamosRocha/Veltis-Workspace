(function () {
  const storageKey = "veltis-workspace-theme";
  const root = document.documentElement;
  const storedTheme = window.localStorage.getItem(storageKey);
  const prefersDark = window.matchMedia("(prefers-color-scheme: dark)").matches;

  if (storedTheme === "dark" || (!storedTheme && prefersDark)) {
    root.classList.add("dark");
  }

  document.querySelectorAll("[data-theme-toggle]").forEach((button) => {
    button.addEventListener("click", () => {
      root.classList.toggle("dark");
      window.localStorage.setItem(storageKey, root.classList.contains("dark") ? "dark" : "light");
    });
  });
})();
