import { Component, OnInit, Inject } from '@angular/core';
import '../../../js/ymaps.js'
import { ReserveManger } from '../../../services/reserve-manager';
import { Car } from '../../../models/car';
import { Remote } from '../../../services/http-client';
declare var ymaps:any
@Component({
  selector: 'app-car-map',
  templateUrl: './car-map.component.html',
  styleUrls: ['./car-map.component.css']
})
export class CarMapComponent implements OnInit {
  ymaps:any;
  constructor(@Inject(Remote)private remote:Remote,private reserveManager:ReserveManger) { }
  ngOnInit() {
    ymaps.ready().then(()=>{
      this.ymaps=new ymaps.Map("main_map",{
        center: [50.450100, 30.523400],
        zoom: 12,
        controls: []
      });
      this.ymaps.container.enterFullscreen();
      this.InitBaloons()
    })

  }
  private InitBaloons(){
    this.reserveManager.GetFreeCars().subscribe((result)=>{
      let cars = result.data as Car[];
      debugger;
      cars.forEach((v,i)=>{
        var myPlacemark = new ymaps.Placemark([v.x,v.y], {
          balloonContentBody: [
             '<div class="card" style="width: 18rem;"> <img class="card-img-top" style="width:100px;"src="https://l-userpic.livejournal.com/111628924/39184714" alt="Card image cap"> <div class="card-body">'+
            '<h5 class="card-title">'+v.brand+'</h5> <p class="card-text">Класс-'+v.class+'</p>'+
            '</div> <ul class="list-group list-group-flush"> <li class="list-group-item">Цена за киллометр - '+v.cost+' тг</li> <li class="list-group-item">Номер - '+v.number+'</li>'+
             '<li class="list-group-item">Vestibulum at eros</li> </ul> <div class="card-body"> <a href="'+this.remote.baseUri+'/ReserveCar?carId='+v.Id+'" class="card-link">Забронировать</a>'+
              '<a href="#" class="card-link">Another link</a>'+
              '</div>'+
               '</div>'
          ].join('')
      }, {
          preset: 'islands#redDotIcon'
      });
      this.ymaps.geoObjects.add(myPlacemark);
      });
    })
  }
}
