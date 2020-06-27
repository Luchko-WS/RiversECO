import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import Map from 'ol/Map';
import View from 'ol/View';
import VectorLayer from 'ol/layer/Vector';
import Style from 'ol/style/Style';
import Icon from 'ol/style/Icon';
import OSM from 'ol/source/OSM';
import * as olProj from 'ol/proj';
import {defaults as defaultInteractions, DragRotateAndZoom} from 'ol/interaction';
import {defaults as defaultControls, ScaleLine, FullScreen} from 'ol/control.js';
import TileLayer from 'ol/layer/Tile';

import { WaterObject } from 'src/app/models/water-object';

@Component({
  selector: 'app-map',
  templateUrl: './map.component.html',
  styleUrls: ['./map.component.css']
})

export class MapComponent implements OnInit {
  private waterObjects: WaterObject[];
  private map: any;

  constructor(private route: ActivatedRoute) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.waterObjects = data['waterObjects'];
      console.log(this.waterObjects);
    });

    this.map = new Map({
      target: 'map-layer',
      /*interactions: defaultInteractions().extend([
        new DragRotateAndZoom()
      ]),*/
      controls: defaultControls({attribution: false, zoom: true}).extend([
        new ScaleLine(),
        new FullScreen()
      ]),
      layers: [
        new TileLayer({
          source: new OSM()
        })
      ],
      view: new View({
        center: olProj.fromLonLat([28.5785, 49.2]),
        zoom: 12
      })
    });
  }
}
