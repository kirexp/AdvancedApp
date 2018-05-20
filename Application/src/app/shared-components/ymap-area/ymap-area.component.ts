import { Component, OnInit, Input, EventEmitter } from '@angular/core';
import '../../js/ymaps.js'
import { Subject } from 'rxjs/Subject';
import { Observable } from 'rxjs/Observable';
import {Coordinates} from '../../models/coordinates'
declare var ymaps:any
@Component({
  selector: 'app-ymap-area',
  templateUrl: './ymap-area.component.html',
  styleUrls: ['./ymap-area.component.css']
})
export class YmapAreaComponent implements OnInit {
   @Input() onAdrressChanged:Observable<any>;
   @Input() startPoint:Coordinates;
   @Input() endPoint:Coordinates;
   @Input() mapIdentifier:string;
  ymaps:any;
  map:any;
  private isLoaded:boolean=false;
  constructor() { }
  ngOnInit() {
    ymaps.ready().then(()=>{
      this.map=new ymaps.Map(this.mapIdentifier==null?'map':this.mapIdentifier,{
        center: [51.128433, 71.430546 ],
        zoom: 12,
        controls: []
      });
      this.isLoaded=true;
      if(this.onAdrressChanged!=null){
        this.onAdrressChanged.subscribe(x=>{
          this.UpdatePlaceMarkFromAddress(x.address);
        })
      }
      if(this.startPoint!=null&&this.endPoint!=null){
        let myGeoObject = new ymaps.GeoObject({
          // Описываем геометрию геообъекта.
          geometry: {
              // Тип геометрии - "Ломаная линия".
              type: "LineString",
              // Указываем координаты вершин ломаной.
              coordinates: [
                [this.startPoint.latitude, this.startPoint.longitude],
                [this.endPoint.latitude, this.endPoint.longitude]
              ]
          },
          // Описываем свойства геообъекта.
          properties:{
              // Содержимое хинта.
              hintContent: "Я геообъект",
              // Содержимое балуна.
              balloonContent: `Направление ${this.startPoint.address} ==>${this.endPoint.address}`
          }
      }, {
          // Задаем опции геообъекта.
          // Включаем возможность перетаскивания ломаной.
          draggable: false,
          // Цвет линии.
          strokeColor: "#ff0000",
          // Ширина линии.
          strokeWidth: 4
      });
      this.map.geoObjects.add(myGeoObject);
      }

    });

  }
  UpdatePlaceMarkFromAddress(newValue){
    // ymaps.geocode(newValue).subscribe(x=>{

    // });
    ymaps.geocode(newValue).then(res=>{
      let firstGeoObject = res.geoObjects.get(0);
      let position = firstGeoObject.geometry._coordinates;
      let placemark = this.map.geoObjects.get(0);
      this.map.setCenter(position);
      if (placemark) {
          placemark.geometry.setCoordinates(position);
      } else {
        this.AddBaloon(position[0],position[1]);
      }
  });
  }
  private AddBaloon(lon,lat){
    debugger
    let placeMark=  new ymaps.Placemark([lon,lat], {
      balloonContentBody: [
        '<h1>LOL</h1>'
      ].join('')
  }, {
      preset: 'islands#redDotIcon'
  });
    this.map.geoObjects.add(placeMark);
  }

}
