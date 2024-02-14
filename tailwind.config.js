/** @type {import('tailwindcss').Config} */
const plugin = require('tailwindcss/plugin')
module.exports = {
    content: [
      "./templates/**/*.{html,js,templ,go}",
      "./templates/common/**/*.{html,js,templ,go}",
    ],
    theme: {
      extend: {
        colors:{
          'nord0': '#2E3440',
          'nord1': '#3B4252',
          'nord2': '#434C5E',
          'nord3': '#4C566A',
          'nord4': '#D8DEE9',
          'nord5': '#E5E9F0',
          'nord6': '#ECEFF4',
          'nord7': '#8FBCBB',
          'nord8': '#88C0D0',
          'nord9': '#81A1C1',
          'nord10': '#A3BE8C',
          'nord11': '#BF616A',
          'nord12': '#D08770',
          'nord13': '#EBCB8B',
          'nord14': '#B48EAD',
          'nord15': '#5E81AC'
        }
      },
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