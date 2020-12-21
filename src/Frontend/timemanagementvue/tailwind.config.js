module.exports = {
  purge: [],
  darkMode: 'media', // or 'media' or 'class'
  theme: {
    extend: {
      colors: {
        'primary-cyan': {
          darkest: '#17adcf',
          dark: '#1ac0e5',
          DEFAULT: '#31c6e8',
          light: '#48cceb',
          lightest: '#5fd3ed',
        },
        'secondary-cyan': {
          darkest: '#17cfcf',
          dark: '#1ae5e5',
          DEFAULT: '#31e8e8',
          light: '#48ebeb',
          lightest: '#5feded',
        },
        'lime-green': {
          darkest: '#17cfcf',
          dark: '#1ae5e5',
          DEFAULT: '#31e89f',
          light: '#48ebeb',
          lightest: '#5feded',
        }
      }
    },
  },
  variants: {
    extend: {},
  },
  plugins: [
  ],
}
