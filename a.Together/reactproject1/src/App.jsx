import { useState } from 'react'
import reactLogo from './assets/react.svg'
import viteLogo from '/vite.svg'
import './App.css'

function App() {
  const login = () => fetch('/api/login', {method: 'post'})
  const test = () => fetch('/api/test')
  return (
    <div className='App'>
    <button onClick={login}> Login</button>
    <button onClick={test}> Test</button>
    </div>
  )
}

export default App
