import { useState } from "react";
import { BrowserRouter, Navigate, Route, Routes } from "react-router-dom";
import AdminPage from "./pages/admin/AdminPage";
import { InicioPage } from "./pages/inicio/InicioPage";
import LoginPage from "./pages/login/LoginPage";
import { MainPage } from "./pages/main/MainPage";
import { RegisterPage } from "./pages/register/RegisterPage";

function App() {
  const data =  JSON.parse(sessionStorage.getItem('data'));
  console.log(data);

  return (
    <>
      <BrowserRouter>
        <Routes>
          {sessionStorage.getItem('data')?
            <Route path="/" element={  <MainPage /> } >
              <Route index element={ <Navigate to='inicio'/> } />
              <Route path="inicio" element={<InicioPage />}/>
              <Route path="admin" element={<AdminPage />} />
            </Route>
          :
            <>
              <Route path="/" element={ <LoginPage />} />
              <Route path="register" element={ <RegisterPage/>}/>
            </>
          
          }

          <Route path="*" element={<Navigate to='/'/>}/> 
        </Routes>
      </BrowserRouter>
    </>
  );
}

export default App;
