/** @type {import('tailwindcss').Config} */
const plugin = require('tailwindcss/plugin')
module.exports = {
    content: [
      "./templates/**/*.{html,js,templ,go}",
      "./templates/common/**/*.{html,js,templ,go}",
    ],
    theme: {
      extend: {},
      fontFamily: {
        sans: ["Quicksand"],
      },
    },
    plugins: [
      require("@tailwindcss/forms"), 
      require("@tailwindcss/typography"),
      plugin(function({ addVariant }) {
        addVariant('htmx-settling', ['&.htmx-settling', '.htmx-settling &'])
        addVariant('htmx-request',  ['&.htmx-request',  '.htmx-request &'])
        addVariant('htmx-swapping', ['&.htmx-swapping', '.htmx-swapping &'])
        addVariant('htmx-added',    ['&.htmx-added',    '.htmx-added &'])
      }),
    ],
  };