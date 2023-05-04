import { AfterViewInit, Component, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Mascota } from 'src/app/interfaces/mascota';
import { MascotaService } from 'src/app/services/mascota.service';

// const listMascota:Mascota[] = [
//   {nombre: 'juan', edad: 24, raza: 'juis',color:'loas',peso: 45},
//   {nombre: 'loco', edad: 67, raza: 'chiwawa',color:'azul',peso: 100},
//   {nombre: 'gato', edad: 45, raza: 'angora',color:'guindo',peso: 4},
//   {nombre: 'juan', edad: 24, raza: 'juis',color:'loas',peso: 45},
//   {nombre: 'juan', edad: 24, raza: 'juis',color:'loas',peso: 45},
//   {nombre: 'juan', edad: 24, raza: 'juis',color:'loas',peso: 45},
//   {nombre: 'juan', edad: 24, raza: 'juis',color:'loas',peso: 45},
//   {nombre: 'juan', edad: 24, raza: 'juis',color:'loas',peso: 45},
//   {nombre: 'juan', edad: 24, raza: 'juis',color:'loas',peso: 45}
// ];

@Component({
  selector: 'app-listado-mascota',
  templateUrl: './listado-mascota.component.html',
  styleUrls: ['./listado-mascota.component.css']
})

export class ListadoMascotaComponent implements AfterViewInit{
  displayedColumns: string[] = ['nombre', 'edad', 'raza', 'color', 'peso', 'acciones'];
  // dataSource=new MatTableDataSource<Mascota>(listMascota);
  dataSource=new MatTableDataSource<Mascota>();
  loading:boolean=false;
  
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
    if(this.dataSource.data.length>0){
      this.dataSource.paginator = this.paginator;
    }
    this.dataSource.sort = this.sort;
    
  }
  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }
  
  ngOnInit(): void {
    this.obtenerMascota();
  }

  //inyectamos la dependencia el service
  constructor(private _snackBar: MatSnackBar, private _serviceMascota: MascotaService) {
    
  }
  //creamos el metodo
  obtenerMascota(){
    this.loading=true;
     this._serviceMascota.getmascotas().subscribe(data=>{
      this.loading=false;
      this.dataSource.data=data;
      // console.log(data[0].nombre);
    },error=>{
      this.loading=false;
      alert("Ocurrio un problema")
    })
  }
  //VERSION ACTUAL PARA USAR ERRORES con subscribe
  // obtenerMascota(){
  //   this.loading=true;
  //    this._serviceMascota.getmascotas().subscribe({
  //     next: (data) => {
  //       this.dataSource.data=data;
  //     },
  //     error: (e) => {
  //       this.loading=false;
  //       alert("OOOPS")
  //     },
  //     complete: () => console.info('complete') 
  //   })
  // }

  eliminarMascota(id:number) {
    this.loading=true;
    this._serviceMascota.deleteMascota(id).subscribe(()=>{
      this.mostrarMensaje();
      this.obtenerMascota();
    })
  }

  mostrarMensaje(){
      this.loading=false;
      this._snackBar.open("Elemento Eliminado", '', {
        duration: 2000,
        horizontalPosition:'end',
        verticalPosition:'bottom'
      });
  }
}
