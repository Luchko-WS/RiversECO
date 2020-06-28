import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import Map from 'ol/Map';
import View from 'ol/View';
import VectorLayer from 'ol/layer/Vector';
import Style from 'ol/style/Style';
import Icon from 'ol/style/Icon';
import OSM from 'ol/source/OSM';
import {Point, Polygon} from 'ol/geom';
import {defaults as defaultInteraction, Select} from 'ol/interaction';
import {defaults as defaultSource, Vector} from 'ol/source';
import Feature from 'ol/feature';
import * as olProj from 'ol/proj';
import {defaults as defaultControls, Rotate, ScaleLine} from 'ol/control';
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
    this.initMap();
    this.route.data.subscribe(data => {
      this.waterObjects = data['waterObjects'];
      this.drawObjects(this.waterObjects);
    });
  }

  initMap() {
    this.map = new Map({
      target: 'map-layer',
      controls: defaultControls({attribution: false, zoom: true}).extend([
        new Rotate(),
        new ScaleLine()
      ]),
      layers: [
        new TileLayer({
          source: new OSM()
        })
      ],
      interactions: defaultInteraction().extend([
        this.adjustSelectInteraction()
      ]),
      view: new View({
        projection: 'EPSG:3857',
        center: olProj.fromLonLat([28.5785, 49.2]),
        zoom: 12
      })
    });
  }

  adjustSelectInteraction(): Select {
    const selectInteraction = new Select({multi: false});
    selectInteraction.on('select', (e) => {
      console.log(e.selected);
    });
    return selectInteraction;
  }

  drawObjects(objects: WaterObject[]) {
    const features = [];
    objects.forEach(obj => {
      //console.error(olProj.transform([28.5785, 49.2], 'EPSG:4326', 'EPSG:3857'));
      features.push(new Feature({
        geometry: new Polygon([obj.geometry])/*.transform('EPSG:4326', 'EPSG:3857')*/,
        name: obj.name,
        population: 4000,
        rainfall: 500
      }));
    });

    const vectorLayer = new VectorLayer({
      source: new Vector({
        features
      })
    });

    this.map.addLayer(vectorLayer);
  }
}
