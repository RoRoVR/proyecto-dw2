import { Outlet } from "react-router-dom";

function InicioPage() {
    return (
        <>
            <h1>Inicio Page</h1>
            <Outlet/>
        </>
    );
}

export default InicioPage;