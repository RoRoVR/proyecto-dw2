import React from 'react'
import { NavLink, Outlet } from 'react-router-dom'

export const MainPage = () => {
    const data =  JSON.parse(sessionStorage.getItem('data'));

    const cerrarSesion = () => {
        sessionStorage.clear();
        window.location.reload(false);
    }
  return (
  <>
    <nav className="navbar navbar-expand-lg bg-body-tertiary navbar-dark bg-dark ">
        <div className="container-fluid">
            <NavLink className="navbar-brand" to='/inicio'><i className="bi bi-film"></i></NavLink>
            <button className="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarSupportedContent" aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
            <span className="navbar-toggler-icon"></span>
            </button>
            <div className="collapse navbar-collapse" id="navbarSupportedContent">
            <ul className="navbar-nav me-auto mb-2 mb-lg-0">
                <li className="nav-item"> 
                    <NavLink className={({ isActive }) => isActive ? 'nav-link active' : 'nav-link'} to='/inicio' >Inicio</NavLink>
                </li>
                {data.token&&
                    <li className="nav-item">
                        <NavLink className={({ isActive }) => isActive ? 'nav-link active' : 'nav-link'} to='/admin' >Administrar</NavLink>
                    </li>
                }
            </ul>
            <div className='d-flex align-items-center  text-white ' >
                <p className='m-0 me-3 ' >{ data.usuario.nombre} {data.usuario.apellido}</p>
                <div className='btn btn-danger' onClick={cerrarSesion} >Salir</div>
            </div>

            </div>
        </div>
    </nav>
        
    <Outlet/>
  </>
  )
}
