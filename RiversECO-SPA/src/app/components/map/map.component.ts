import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import Map from 'ol/Map';
import View from 'ol/View';
import VectorLayer from 'ol/layer/Vector';
import Style from 'ol/style/Style';
import Icon from 'ol/style/Icon';
import OSM from 'ol/source/OSM';
import {Point} from 'ol/geom';
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
      this.addMarkers(this.waterObjects);
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
      view: new View({
        center: olProj.fromLonLat([28.5785, 49.2]),
        zoom: 12
      })
    });
  }

  addMarkers(objects: WaterObject[]) {
    let iconFeatures = [];
    objects.forEach(obj => {
      iconFeatures.push(new Feature({
        geometry: new Point(olProj.transform([obj.lat, obj.long], 'EPSG:4326',
        'EPSG:3857')),
        name: obj.name,
        population: 4000,
        rainfall: 500
      }));
    });

    const vectorSource = new Vector({
      features: iconFeatures
    });

    const iconStyle = new Style({
      image: new Icon({
        anchor: [0.5, 46],
        anchorXUnits: 'fraction',
        anchorYUnits: 'pixels',
        opacity: 1,
        src: '../../../assets/red-marker-32.png'
      })
    });

    const vectorLayer = new VectorLayer({
      source: vectorSource,
      style: iconStyle
    });

    this.map.addLayer(vectorLayer);
  }
}
