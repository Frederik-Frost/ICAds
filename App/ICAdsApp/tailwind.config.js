/** @type {import('tailwindcss').Config} */
module.exports = {
  content: ['./src/**/*.{html,js,vue}'],
  theme: {
    colors: {
      white: '#fff',
      offWhite: '#fafafa',
      alabaster: '#f2f0e7',
      charcoal: '#3f464a',
      charcoal75: 'rgba(63, 70, 74, 0.75)',
      charcoal50: 'rgba(63, 70, 74, 0.5)',
      charcoal25: 'rgba(63, 70, 74, 0.25)',
      charcoal115: '#353b3f',
      charcoalBorder: '#485258',
      hoverWhite: 'rgba(204, 204, 204, 0.2)',
      flame: '#eb5e34',
      orangeWeb: '#f6a21c',
      primary1: '#1CCFF6',
      primary2: '#23A9F4',
      secondary1: '#1F34EC',
      secondary2: '#394BEB',
    },
    fontFamily: {
      primary: ['Roboto', 'sans-serif'],
    },
    variants: {
      extend: {
        display: ['group-hover'],
      },
    },
    // extend: {
    //   typography: (theme) => ({
    //     DEFUALT: {
    //       css: {
    //         color: theme("colors.flame"),
    //       },
    //     },
    //   }),
    // },
  },
  plugins: [],
};
