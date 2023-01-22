import { BrowserRouter, Navigate, Route, Routes } from "react-router-dom";
import AdminPage from "./pages/admin/AdminPage";
import InicioPage from "./pages/inicio/InicioPage";

function App() {
  const pelicula = {
    id:'id',
    nombre: 'nombre',
    director: 'director',
    idGenero: 'idGenero',
    descripcion:'descripcion',
    foto:'URL de foto',
    fechaEstreno:'la fecha en la que se estreno o estrenara'
  }

  return (
    <>
      <BrowserRouter>
        <Routes>
        <Route path="/" element={  <InicioPage/> } >

          <Route index element={ <Navigate to='/admin'/> } />
            <Route path="admin" element={<AdminPage/>} />


          </Route>
        </Routes>
      </BrowserRouter>
    </>
  );
}

export default App;
