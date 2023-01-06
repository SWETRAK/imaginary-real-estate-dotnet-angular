import { ComponentFixture, TestBed } from '@angular/core/testing';

import { IreHomePageComponent } from './ire-home-page.component';

describe('IreHomePageComponent', () => {
  let component: IreHomePageComponent;
  let fixture: ComponentFixture<IreHomePageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ IreHomePageComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(IreHomePageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
