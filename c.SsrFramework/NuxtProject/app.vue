<template>
  <div>
    <button @click="login">Login</button>
    <button @click="test">Test</button>
  </div>
</template>

<script setup> 
const login = () => fetch('http://localhost:5113/api/login', {method: 'post', credentials: 'include'})
const test = () => fetch('http://localhost:5113/api/test', {credentials: 'include'})
</script>