/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
    './app/**/*.{js,ts,jsx,tsx,mdx}',
    './pages/**/*.{js,ts,jsx,tsx,mdx}',
    './components/**/*.{js,ts,jsx,tsx,mdx}',
  ],
  theme: {
    extend: {
      screens: {
        'xs': '320px',

        'sm': '480px',

        'md': '768px',

        'lg': '1024px',

        'xl': '1280px',

        '2xl': '1536px'
      },
      colors: {
        'primary': '#E7FAFE'
      },
      flex: {
        '1/2': '0 1 50%'
      }
    },
  },
  plugins: [],
}