import { MetaProvider, Title } from "@solidjs/meta";
import { Router } from "@solidjs/router";
import { FileRoutes } from "@solidjs/start/router";
import { Suspense } from "solid-js";
import "./app.css";

 function App() {
  const login = () => fetch('/api/login', {method: 'post'})
  return (
   <div class={styles.App}>
   <button onClick={login}>Login</button>
   </div>
  );
}
